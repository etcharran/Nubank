using Nubank.Contract;
using Nubank.Persistence.Repositories;
using Nubank.Tools.Exceptions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Nubank.Tests.Persitence
{
    public class AccountRepositoryTests
    {
        private readonly IAccountRepository accountRepository;

        public AccountRepositoryTests()
        {
            accountRepository = new SingleAccountRepository();
        }

        [Fact]
        public void NullAccount()
        {
            Assert.Throws<NullAccountException>(() => accountRepository.Get());
        }

        [Fact]
        public void AnyAccount()
        {
            Assert.True(!accountRepository.Any());
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void AnyAccountWithData(Account active, Account inactive)
        {
            accountRepository.Create(active);
            Assert.True(accountRepository.Any());
            accountRepository.Create(inactive);
            Assert.True(accountRepository.Any());
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void CreateAccount(Account active, Account inactive)
        {
            accountRepository.Create(active);
            Assert.True(accountRepository.Any());
            Assert.Equal(active, accountRepository.Get(), new AccountComparer());
            accountRepository.Create(inactive);
            Assert.NotEqual(active, accountRepository.Get(), new AccountComparer());
            Assert.Equal(inactive, accountRepository.Get(), new AccountComparer());
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void UpdateAccount(Account active, Account inactive)
        {
            accountRepository.Create(active);
            Assert.True(accountRepository.Any());
            Assert.Equal(active, accountRepository.Get(), new AccountComparer());
            accountRepository.Update(inactive);
            Assert.NotEqual(active, accountRepository.Get(), new AccountComparer());
            Assert.Equal(inactive, accountRepository.Get(), new AccountComparer());
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

    public class AccountComparer : IEqualityComparer<Account>
    {
        public bool Equals([AllowNull] Account x, [AllowNull] Account y) => x.ActiveCard == y.ActiveCard && x.AvailableLimit == y.AvailableLimit;

        public int GetHashCode([DisallowNull] Account obj)
        {
            return obj.GetHashCode();
        }
    }
}
