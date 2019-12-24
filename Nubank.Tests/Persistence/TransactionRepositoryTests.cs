using Nubank.Contract;
using Nubank.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Nubank.Tests.Persitence
{
    public class TransactionRepositoryTests
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionRepositoryTests()
        {
            transactionRepository = new TransactionRepository();
        }

        [Fact]
        public void EmptyGetAll()
        {
            Assert.True(transactionRepository.GetAll().Count == 0);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Create(Transaction first, Transaction second)
        {
            transactionRepository.Create(first);
            Assert.True(transactionRepository.GetAll().Count == 1);
            Assert.Equal(transactionRepository.GetAll().Where(t => t.Merchant == first.Merchant).First(), first, new TransactionComparer());

            transactionRepository.Create(second);
            Assert.True(transactionRepository.GetAll().Count == 2);
            Assert.Equal(transactionRepository.GetAll().Where(t => t.Merchant == second.Merchant).First(), second, new TransactionComparer());

        }



        public static IEnumerable<object[]> Data()
        {
            yield return new object[]
            {
                new Transaction { Amount = 20, Merchant = "First", Time = DateTime.Now },
                new Transaction { Amount = 20, Merchant = "Second", Time = DateTime.Today }
            };
        }
    }



    public class TransactionComparer : IEqualityComparer<Transaction>
    {
        public bool Equals([AllowNull] Transaction x, [AllowNull] Transaction y)
            => x.Time == y.Time && x.Merchant == y.Merchant && y.Amount == x.Amount;

        public int GetHashCode([DisallowNull] Transaction obj)
        {
            return obj.GetHashCode();
        }
    }
}
