using TaskAPI;
using Xunit;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestProject1
{
    public class DogCeoSingletonTests
    {

        //var observerField = typeof(DogCeo).GetField("_observers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        //var observers = (List<Action<string>>)observerField.GetValue(instance);
        //Assert.True(observers.Count == 3);
        [Fact]
        public void DogCeo_Instance_Should_Return_Same_Instance()
        {
            // Arrange & Act
            var instance1 = DogCeo.Instance;
            var instance2 = DogCeo.Instance;


            // Assert
            Assert.Same(instance1, instance2);
            Assert.NotNull(instance1);
        }
        [Fact]
        // test add one observer
        public void AddObsrever_Should_Add_Observer()
        {
            var instance = DogCeo.Instance;
            // «Ã⁄· ﬁ«∆„… «·„—«ﬁ»Ì‰ ›«—€… √Ê·«
            instance._observers.Clear();
            var observer = new Action<string>(url => { });
            instance.AddObsrever(observer);
            Assert.Contains(observer, instance._observers);
            Assert.NotNull(instance._observers);
            Assert.True(instance._observers.Count == 1);
        }



        ////test add more than one observer
        [Fact]
        public void AddObsrever_Should_Add_Multiple_Observers()
        {
            var instance = DogCeo.Instance;
            //    // «Ã⁄· ﬁ«∆„… «·„—«ﬁ»Ì‰ ›«—€… √Ê·«
            instance._observers.Clear();

            var observer1 = new Action<string>(url => { Console.WriteLine(" Dog image Url 1:" + url); });
        var observer2 = new Action<string>(url => { Console.WriteLine(" Dog image Url 2:" + url); });
        var observer3 = new Action<string>(url => { Console.WriteLine(" Dog image Url 3:" + url); });

        instance.AddObsrever(observer1);
            instance.AddObsrever(observer2);
            instance.AddObsrever(observer3);
            Assert.Contains(observer1, instance._observers);
            Assert.Contains(observer2, instance._observers);
            Assert.Contains(observer3, instance._observers);
            Assert.NotNull(instance._observers);
            Assert.True(instance._observers.Count == 3);

        //    //Assert.True(instance._observers.Count == 4);
        }


        [Fact]
        public async Task FetchDogImageAsync_ShouldNotifyObserversWithValidUrl()
        {
            // Arrange
            var manager = DogCeo.Instance;
            // „ €Ì— ·Ì· ﬁÿ «·—«»ÿ «·„—”·
            string receivedUrl = "";
            manager._observers.Clear();


            //‰÷Ì› receivedUrl ﬂ„—«ﬁ» ·Ì· ﬁÿ «·—«»ÿ
            manager.AddObsrever(url => receivedUrl = url);

            // «” œ⁄«¡ «·œ«·… ·Ã·» ’Ê—… ﬂ·»
            await manager.FetchDogImageAsync();


            //  Õﬁﬁ √‰ receivedUrl €Ì— ›«—€ Ê«‰Â «” ·„ —«»ÿ 
            Assert.False(string.IsNullOrEmpty(receivedUrl));

            //  Õﬁﬁ √‰ «·—«»ÿ Ì»œ√ »‹ https (·· √ﬂœ „‰ ’Õ Â ‘ﬂ·Ì«)
            Assert.StartsWith("https://images.dog.ceo/breeds/", receivedUrl);
        }
       

    }
}