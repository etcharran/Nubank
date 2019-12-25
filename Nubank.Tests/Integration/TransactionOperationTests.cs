using Microsoft.Extensions.DependencyInjection;
using Nubank.Contract;
using Nubank.Domain.Logic;
using Nubank.Domain.Validation;
using Nubank.Tools.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

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
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());
        }

        [Theory]
        [MemberData(nameof(LimitAmountData))]
        public void LimitAmountTransaction(Account account, Transaction first, Transaction second)
        {
            var correctAccount = account.Clone();
            correctAccount.AvailableLimit -= first.Amount;
            var correctResponse = new AccountResponse
            {
                Account = correctAccount,
                Violations = new List<string>()
            };

            operationLogic.Operate(account);
            var actualResponse = operationLogic.Operate(first) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            correctResponse.Violations.Add(InsufficientLimitValidation.name);
            actualResponse = operationLogic.Operate(second) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

        }

        [Theory]
        [MemberData(nameof(InactiveCardData))]
        public void InactiveCard(Account account, Transaction transaction)
        {
            var correctAccount = account.Clone();
            var correctResponse = new AccountResponse
            {
                Account = correctAccount,
                Violations = new List<string>() { InactiveCardValidation.name }
            };

            operationLogic.Operate(account);
            var actualResponse = operationLogic.Operate(transaction) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());
        }

        [Theory]
        [MemberData(nameof(DoubledTransactionData))]
        public void DoubledTransaction(Account account, Transaction first, Transaction second, Transaction third, Transaction fourth)
        {
            var correctAccount = account.Clone();
            correctAccount.AvailableLimit -= first.Amount;
            var correctResponse = new AccountResponse
            {
                Account = correctAccount,
                Violations = new List<string>()
            };

            operationLogic.Operate(account);
            var actualResponse = operationLogic.Operate(first) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            correctAccount.AvailableLimit -= second.Amount;
            operationLogic.Operate(account);
            actualResponse = operationLogic.Operate(second) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            correctAccount.AvailableLimit -= third.Amount;
            operationLogic.Operate(account);
            actualResponse = operationLogic.Operate(third) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            correctResponse.Violations.Add(DoubledTransactionValidation.name);
            operationLogic.Operate(account);
            actualResponse = operationLogic.Operate(fourth) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());
        }

        [Theory]
        [MemberData(nameof(HighFrequencyData))]
        public void HighFrequency(Account account, Transaction first, Transaction second, Transaction third, Transaction fourth)
        {
            var correctAccount = account.Clone();
            correctAccount.AvailableLimit -= first.Amount;
            var correctResponse = new AccountResponse
            {
                Account = correctAccount,
                Violations = new List<string>()
            };

            operationLogic.Operate(account);
            var actualResponse = operationLogic.Operate(first) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            correctAccount.AvailableLimit -= second.Amount;
            operationLogic.Operate(account);
            actualResponse = operationLogic.Operate(second) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            correctAccount.AvailableLimit -= third.Amount;
            operationLogic.Operate(account);
            actualResponse = operationLogic.Operate(third) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            correctResponse.Violations.Add(HighFrequencyValidation.name);
            operationLogic.Operate(account);
            actualResponse = operationLogic.Operate(fourth) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());
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
                new Transaction { Merchant = "First", Amount = 20, Time = DateTime.Now  },
                new Transaction { Merchant = "Second", Amount = 15, Time = DateTime.Now  }
            };
        }

        public static IEnumerable<object[]> InactiveCardData()
        {
            yield return new object[]
            {
                new Account { ActiveCard = false, AvailableLimit = 100 },
                new Transaction { Merchant = "First", Amount = 100, Time = DateTime.Now  }
            };
        }

        public static IEnumerable<object[]> HighFrequencyData()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 100 },
                new Transaction { Merchant = "First", Amount = 10, Time = DateTime.Now  },
                new Transaction { Merchant = "Second", Amount = 20, Time = DateTime.Now.AddMinutes(1)  },
                new Transaction { Merchant = "Third", Amount = 10, Time = DateTime.Now.AddSeconds(2)  },
                new Transaction { Merchant = "Fourth", Amount = 10, Time = DateTime.Now.AddSeconds(60)  }
            };
        }

        public static IEnumerable<object[]> DoubledTransactionData()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 100 },
                new Transaction { Merchant = "First", Amount = 10, Time = DateTime.Now  },
                new Transaction { Merchant = "First", Amount = 20, Time = DateTime.Now.AddMinutes(3)  },
                new Transaction { Merchant = "First", Amount = 10, Time = DateTime.Now.AddSeconds(2)  },
                new Transaction { Merchant = "First", Amount = 10, Time = DateTime.Now.AddSeconds(60)  }
            };
        }
    }
}