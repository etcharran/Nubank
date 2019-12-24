using Nubank.Domain.Logic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Nubank.Contract;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Nubank.Tests.Persitence;
using System.Linq;
using Nubank.Domain.Validation;

namespace Nubank.Tests.Integration
{
    public class AccountOperationTests : IntegrationTests
    {
        private readonly IOperationLogic operationLogic;

        public AccountOperationTests()
            :base()
        {
            operationLogic = serviceProvider.GetService<IOperationLogic>();
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void CreateAccount(Account activeAccount, Account inactiveAccount)
        {
            var correctResponse = new AccountResponse { Account = activeAccount, Violations = new List<string>() };
            var actualResponse = operationLogic.Operate(activeAccount) as AccountResponse;
            Assert.Equal(correctResponse,actualResponse,new AccountResponseComparer());
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void InitializedAccount(Account activeAccount, Account inactiveAccount)
        {
            var correctResponse = new AccountResponse { Account = activeAccount.Clone(), Violations = new List<string>() };
            var actualResponse = operationLogic.Operate(activeAccount) as AccountResponse;
            Assert.Equal(correctResponse,actualResponse,new AccountResponseComparer());

            var inactiveCreate = operationLogic.Operate(inactiveAccount) as AccountResponse;
            var incorrectResponse = new AccountResponse { Account = correctResponse.Account.Clone(), Violations = new List<string>(){"account-already-initialized"}};
            Assert.Equal(inactiveCreate, incorrectResponse, new AccountResponseComparer());
            Assert.NotEqual(inactiveCreate, correctResponse, new AccountResponseComparer());
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[]
            {
                new Account { ActiveCard = true, AvailableLimit = 100 },
                new Account { ActiveCard = false, AvailableLimit = 100 }
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