using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace TaskAPI
{
    public class DogCeo
    {
        //Singleton
        private static DogCeo instance;
        private static readonly object _lock = new object();
        private HttpClient _httpClient = new HttpClient();

        //Observer
        public List<Action<string>> _observers = new List<Action<string>>();

        private DogCeo() { }

        public static DogCeo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new DogCeo();
                        }
                    }
                }
                return instance;
            }
        }




        public void AddObsrever(Action<string> observer)
        {
            _observers.Add(observer);
        }
        //تنبيه المراقبين Api من url دالة جلب



        public async Task FetchDogImageAsync()
        {
            string url = "https://dog.ceo/api/breeds/image/random";
            string json = await _httpClient.GetStringAsync(url);
            dynamic result = JsonConvert.DeserializeObject(json);
            string ImageUrl = result.message;

            foreach (var observer in _observers)
            {
                observer(ImageUrl);
            }
        }
       



    }
}