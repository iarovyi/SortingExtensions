using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SortingExtensions.Implementation;

namespace SortingExtensions.Tests.Implementation
{
    [TestFixture]
    class RandomProviderTests
    {
        [Test]
        public void RandomProvider_Returns_Different_Random_Objects_For_Different_Threads()
        {
            Task<Random> task1 = Task<Random>.Factory.StartNew(RandomProvider.GetThreadRandom),
                         task2 = Task<Random>.Factory.StartNew(RandomProvider.GetThreadRandom);

            Task.WhenAll(task1, task2);

            Assert.That(task1.Result != task2.Result);
        }

        [Test]
        public void RandomProvider_Returns_The_Same_Random_Obect_For_The_Same_Thread()
        {
            Random random1 = RandomProvider.GetThreadRandom(),
                   random2 = RandomProvider.GetThreadRandom();

            Assert.That(random1 == random2);
        }
    }
}
