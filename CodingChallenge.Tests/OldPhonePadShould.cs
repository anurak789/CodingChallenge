using NUnit.Framework;
using System;
using System.Threading;
using System.Windows.Threading;

namespace CodingChallenge.Tests
{
    [TestFixture]
    public class OldPhonePadShould
    {
        [Test]
        public void ReturnLetters()
        {
            //The calling thread must be STA, because many UI components require this.'
            StaThreadWrapper(() =>
            {
                //Arrange
                var sut = new CodingChallenge.MainWindow();
                //Act
                string output = sut.OldPhonePad("4433555 555666");
                //Assert
                Assert.That(output, Is.EqualTo("HELLO"));
            });
        }

        [Test]
        public void ReturnEmpty()
        {
            StaThreadWrapper(() =>
            {
                //Arrange
                var sut = new CodingChallenge.MainWindow();
                //Act
                string output = sut.OldPhonePad(")))000#");
                //Assert
                Assert.AreEqual("", output);
            });
        }

        private static void StaThreadWrapper(Action action)
        {
            var t = new Thread(o =>
            {
                action();
                Dispatcher.Run();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
    }
}
