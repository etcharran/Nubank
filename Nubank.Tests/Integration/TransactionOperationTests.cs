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
        [MemberData(nameof(ValidTransactionData))]
        public void ValidTransaction(Account account, Transaction transaction)
        {
            var correctAccount = account.Clone();
            correctAccount.AvailableLimit -= transaction.Amount;
            var correctResponse = new AccountResponse { Account = correctAccount, Violations = new List<string>() };
            operationLogic.Operate(account);
            var actualResponse = operationLogic.Operate(transaction) as AccountResponse;
            Assert.Equal(correctResponse,actualResponse,new AccountResponseComparer());
        }

        [Theory]
        [MemberData(nameof(LimitAmountData))]
        public void LimitAmountTransaction(Account account, Transaction first, Transaction second)
        {
            var correctAccount = account.Clone();

            var correctResponse = new AccountResponse { 
                Account = correctAccount, 
                Violations = new List<string>() 
            };

            operationLogic.Operate(account);
            var actualResponse = operationLogic.Operate(first) as AccountResponse;
            Assert.Equal(correctResponse,actualResponse,new AccountResponseComparer());

            correctResponse.Violations.Add("insufficient-limit");
            actualResponse = operationLogic.Operate(second) as AccountResponse;
            Assert.Equal(correctResponse,actualResponse,new AccountResponseComparer());

        }

        public static IEnumerable<object[]> OnlyTransaction()
        {
            yield return new object[]
            {
                new Transaction { Merchant = "First", Amount = 100, Time = DateTime.Now  }
            };
        }

        public static IEnumerable<object[]> ValidTransactionData()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 100 },
                new Transaction { Merchant = "First", Amount = 100, Time = DateTime.Now  }
            };
        }

        public static IEnumerable<object[]> LimitAmountData()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 30 },
                new Transaction { Merchant = "First", Amount = 20, Time = DateTime.Now  }
                new Transaction { Merchant = "Second", Amount = 15, Time = DateTime.Now  }
            };
        }
    }
}