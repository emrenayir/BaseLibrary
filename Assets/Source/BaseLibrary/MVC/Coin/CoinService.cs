using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Source.BaseLibrary.MVC.Coin
{
    public interface ICoinService
    {
        void Save(ICoinModel model);
        ICoinModel Load();
    }

    public class CoinService : ICoinService
    {
        private const string SavePath = "coinSave.dat";

        public void Save(ICoinModel model)
        {
            CoinData data = model.Serialize();

            // Use BinaryFormatter to serialize the data and save it to a file
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = File.Create(SavePath))
            {
                formatter.Serialize(fileStream, data);
            }
        }

        public ICoinModel Load()
        {
            if (File.Exists(SavePath))
            {
                // Use BinaryFormatter to deserialize the data from the file
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(SavePath, FileMode.Open))
                {
                    CoinData data = (CoinData)formatter.Deserialize(fileStream);

                    // Create a new CoinModel and deserialize the data into it
                    ICoinModel model = new CoinModel();
                    model.DeSerialize(data);

                    return model;
                }
            }
            else
            {
                // If the file doesn't exist, create a new CoinModel
                return new CoinModel();
            }
        }
    }
}