using Microsoft.Extensions.DependencyInjection;
using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence.Repositories;
using Nubank.Tools.Exceptions;
using System;
using System.Collections.Generic;

namespace Nubank.Domain.Operations
{
    public abstract class Operation<T> : IOperation<T>
        where T : IData
    {
        public readonly IAccountRepository accountRepository;
        internal readonly IServiceProvider provider;

        public Operation(IAccountRepository accountRepository, IServiceProvider provider)
        {
            this.accountRepository = accountRepository;
            this.ValidationFixture = new List<IBusinessValidation<T>>();
            this.provider = provider;
            InitializeFixture();
        }

        public IResponse<Account> Process(T data)
        {
            var isValid = true;
            List<string> violetions = new List<string>();
            foreach (var validation in ValidationFixture)
            {
                var validationResponse = validation.Validate(data);
                if (!validationResponse.Success)
                {
                    isValid = false;
                    violetions.Add(validationResponse.Validation);
                }
            }
            if (isValid)
                this.Execute(data);

            return new AccountResponse { Account = accountRepository.Any() ? accountRepository.Get() : null, Violations = violetions };
        }

        public IList<IBusinessValidation<T>> ValidationFixture { get; set; }

        /// <summary>
        /// Executes the actual operation
        /// </summary>
        public abstract void Execute(T data);

        /// <summary>
        /// Initialize Validation Fixture
        /// </summary>
        public abstract void InitializeFixture();

        internal IList<IBusinessValidation<T>> AddToFixture<K>()
            where K : IBusinessValidation<T>
        {
            ValidationFixture.Add(provider.GetService<K>());
            return ValidationFixture;
        }
    }
}
