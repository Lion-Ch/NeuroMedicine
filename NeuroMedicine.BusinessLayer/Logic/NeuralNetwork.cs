using DataLayer.Models.Classes;
using Keras.Models;
using Keras.PreProcessing.Image;
using Numpy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NeuroMedicine.BusinessLayer.Logic
{
    public class NeuralNetwork
    {
        BaseModel _model;
        public NeuralNetwork()
        {
            _model = Sequential.LoadModel(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель");
        }

        public void Analis(Patient patients)
        {
            //var model = Sequential.LoadModel(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель");
            //var datagen = new ImageDataGenerator(rescale: 1.0f / 255);
            //var testGenerator = datagen.FlowFromDirectory(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\train\Test",
            //    target_size: (150, 150).ToTuple(),
            //    batch_size: 16,
            //    class_mode: "binary");
            //foreach(var item in patients)
            //{
            if (File.Exists(patients.PhotoUrl))
            {
                var img = ImageUtil.LoadImg(patients.PhotoUrl, target_size: new Keras.Shape(150, 150, 3));
                NDarray x = ImageUtil.ImageToArray(img);
                x = x.reshape(1, x.shape[0], x.shape[1], x.shape[2]).astype(np.float32) / 255f;
                var result = _model.PredictGenerator(x).reshape(1);
                patients.ProbobilityDisease = result.asscalar<float>() * 100f;
                //for (int i = 0; i < result.len; i++)
                //{
                //    Console.WriteLine($"Вероятность пневмонии: {result[i]}");
                //}
            }
            //}

            //for (int i = 0; i < result.len; i++)
            //{
            //    Console.WriteLine($"Вероятность пневмонии: {result[i]}");
            //}
        }

    }
}
