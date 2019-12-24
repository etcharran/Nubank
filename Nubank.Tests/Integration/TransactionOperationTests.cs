using Nubank.Domain.Logic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Nubank.Tools.Exceptions;
using System.Collections.Generic;
using Nubank.Contract;
using System;

namespace Nubank.Tests.Integration
{
    public class TransctionOperationTests : IntegrationTests
    {
        private readonly IOperationLogic operationLogic;
        public TransctionOperationTests()
            : base()
        {
            operationLogic = serviceProvider.GetService<IOperationLogic>();
        }

        [Theory]
        [MemberData(nameof(OnlyTransaction))]
        public void NotCreatedAccount(Transaction transaction)
        {
            Assert.Throws<NullAccountException>(() => operationLogic.Operate(transaction));
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ValidTransaction(Account account, Transaction transaction)
        {
            var correctAccount = account.Clone();
            correctAccount.AvailableLimit -= transaction.Amount;
            var correctResponse = new AccountResponse { Account = correctAccount.Clone(), Violations = new List<string>() };
            operationLogic.Operate(account);
            var actualResponse = operationLogic.Operate(transaction) as AccountResponse;
            Assert.Equal(correctResponse,actualResponse,new AccountResponseComparer());
        }

        public static IEnumerable<object[]> OnlyTransaction()
        {
            yield return new object[]
            {
                new Transaction { Merchant = "First", Amount = 100, Time = DateTime.Now  }
            };
        }

         public static IEnumerable<object[]> Data()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 100 },
                new Transaction { Merchant = "First", Amount = 100, Time = DateTime.Now  }
            };
        }
    }
}