using Keras.Models;
using Keras.PreProcessing.Image;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NeuroMedicine.BusinessLayer.Logic
{
    public class NeuralNetwork
    {
        public void Analis()
        {
            var model = Sequential.LoadModel(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель");
            var datagen = new ImageDataGenerator(rescale: 1.0f / 255);
            var testGenerator = datagen.FlowFromDirectory(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\train\Test",
                target_size: (150, 150).ToTuple(),
                batch_size: 16,
                class_mode: "binary");
            //var img = ImageUtil.LoadImg(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\Clients\Data\IM-0003-0001.jpeg");
            //NDarray x = ImageUtil.ImageToArray(img);
            //x = x.reshape(x.shape[0], 150, 150,1).astype(np.float32) / 255f;
            var result = model.PredictGenerator(testGenerator);
            for (int i = 0; i < result.len; i++)
            {
                Console.WriteLine($"Вероятность пневмонии: {result[i]}");
            }
        }
        //public Image ResizeImage(Image img, int width, int height)
        //{
        //    Bitmap b = new Bitmap(width, height);
        //    using (Graphics g = Graphics.FromImage((Image)b))
        //    {
        //        g.DrawImage(img, 0, 0, width, height);
        //    }

        //    return (Image)b;
        //}
    }
}
