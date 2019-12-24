﻿using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence.Repositories;
using Nubank.Tools.Exceptions;
using System;
using System.Collections.Generic;

namespace Nubank.Domain.Operations
{
    public abstract class Operation<T> : IOperation<T> where T : IData
    {
        public readonly IAccountRepository accountRepository;
        private bool HasBeenBuilt { get; set; }
        public T Data { get; set; }


        public Operation(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
            this.ValidationFixture = new List<IBusinessValidation<T>>();
        }

        public IOperation<T> Build(T data)
        {
            Data = data;
            InitializeFixture();
            HasBeenBuilt = true;
            return this;
        }

        public IResponse<Account> Process()
        {
            if (this.HasBeenBuilt)
            {
                var isValid = true;
                List<string> violetions = new List<string>();
                foreach (var validation in ValidationFixture)
                {
                    var validationResponse = validation.Validate(Data);
                    if (!validationResponse.Success)
                    {
                        isValid = false;
                        violetions.Add(validationResponse.Validation);
                    }
                }
                if (isValid)
                    this.Execute();

                return new AccountResponse { Account = accountRepository.Get(), Violations = violetions };
            }
            else
                throw new NonBuiltProcessException();
        }

        public IList<IBusinessValidation<T>> ValidationFixture { get; set; }

        /// <summary>
        /// Executes the actual operation
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Initialize Validation Fixture
        /// </summary>
        public abstract void InitializeFixture();
    }
}
