using Microsoft.Extensions.DependencyInjection;
using Nubank.Contract;
using Nubank.Domain.Logic;
using Nubank.Domain.Validation;
using Nubank.Tests.Persitence;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Nubank.Tests.Integration
{
    public class AccountOperationTests : IntegrationTests
    {
        private readonly IOperationLogic operationLogic;

        public AccountOperationTests()
            : base()
        {
            operationLogic = serviceProvider.GetService<IOperationLogic>();
        }

        [Theory]
        [MemberData(nameof(ValidAccountData))]
        public void CreateAccount(Account activeAccount)
        {
            var correctResponse = new AccountResponse { Account = activeAccount, Violations = new List<string>() };
            var actualResponse = operationLogic.Operate(activeAccount) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());
        }

        [Theory]
        [MemberData(nameof(InitializedAccountData))]
        public void InitializedAccount(Account activeAccount, Account inactiveAccount)
        {
            var correctResponse = new AccountResponse { Account = activeAccount.Clone(), Violations = new List<string>() };
            var actualResponse = operationLogic.Operate(activeAccount) as AccountResponse;
            Assert.Equal(correctResponse, actualResponse, new AccountResponseComparer());

            var inactiveCreate = operationLogic.Operate(inactiveAccount) as AccountResponse;
            var incorrectResponse = new AccountResponse
            {
                Account = correctResponse.Account.Clone(),
                Violations = new List<string>() { InitializedAccountValidation.name }
            };
            Assert.Equal(inactiveCreate, incorrectResponse, new AccountResponseComparer());
            Assert.NotEqual(inactiveCreate, correctResponse, new AccountResponseComparer());
        }

        public static IEnumerable<object[]> InitializedAccountData()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 100 },
                new Account { ActiveCard = false, AvailableLimit = 100 }
            };
        }

        public static IEnumerable<object[]> ValidAccountData()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 100 }
            };
        }
    }

    public class AccountResponseComparer : IEqualityComparer<AccountResponse>
    {
        public bool Equals([AllowNull] AccountResponse x, [AllowNull] AccountResponse y)
        {
            return new AccountComparer().Equals(x.Account, y.Account) && x.Violations.SequenceEqual(y.Violations);
        }

        public int GetHashCode([DisallowNull] AccountResponse obj)
        {
            return obj.GetHashCode();
        }
    }
}