using Keras;
using Keras.Layers;
using Keras.Models;
using Keras.PreProcessing.Image;
using Microsoft.Scripting.Hosting;
using Numpy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace NeuroMedicine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseModel model;
        public MainWindow()
        {
            InitializeComponent();
            //model = Sequential.LoadModel(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region Нейросеть
            //Sequential model = new Sequential();
            //model.Add(new Conv2D(input_shape: new Shape(150, 150, 3), filters: 32, kernel_size: (3, 3).ToTuple()));
            //model.Add(new Activation("relu"));
            //model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));

            //model.Add(new Conv2D(filters: 32, kernel_size: (3, 3).ToTuple()));
            //model.Add(new Activation("relu"));
            //model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));

            //model.Add(new Conv2D(filters: 64, kernel_size: (3, 3).ToTuple()));
            //model.Add(new Activation("relu"));
            //model.Add(new MaxPooling2D(pool_size: (2, 2).ToTuple()));

            //model.Add(new Flatten());
            //model.Add(new Dense(64));
            //model.Add(new Activation("relu"));
            //model.Add(new Dropout(0.5f));

            //model.Add(new Dense(1));

            //model.Add(new Activation("sigmoid"));

            //model.Compile(loss: "binary_crossentropy", optimizer: "adam", metrics: new string[] { "accuracy" });
            //model.Summary();


            //string trainPath = @"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\train";
            //string testPath = @"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\test";
            //string valPath = @"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\val";
            //var sizeImage = (150, 150).ToTuple();
            //int batch = 16;

            //var trainDatagen = new ImageDataGenerator(
            //    rescale: 1.0f / 255,
            //    zoom_range: 0.2f,
            //    horizontal_flip: true);

            //var trainData = trainDatagen.FlowFromDirectory(trainPath,
            //    target_size: sizeImage,
            //    batch_size: batch,
            //    class_mode: "binary");
            //var testData = trainDatagen.FlowFromDirectory(testPath,
            //    target_size: sizeImage,
            //    batch_size: batch,
            //    class_mode: "binary");
            //var valData = trainDatagen.FlowFromDirectory(valPath,
            //    target_size: sizeImage,
            //    batch_size: batch,
            //    class_mode: "binary");


            //model.FitGenerator(trainData,
            //    steps_per_epoch: 326,
            //    validation_data: valData,
            //    epochs: 20,
            //    validation_steps: 1);

            //model.Save(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель");
            //model.SaveWeight(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Модель\weights.h5");
            #endregion

            #region Запуск
            var datagen = new ImageDataGenerator(rescale: 1.0f / 255);
            var testGenerator = datagen.FlowFromDirectory(@"C:\Users\levac\OneDrive\Рабочий стол\Учеба\Диплом\Dataset\Clients",
                target_size: (150, 150).ToTuple(),
                batch_size: 16,
                class_mode: "binary");
            var result = model.PredictGenerator(testGenerator);
            for (int i = 0; i < result.len; i++)
            {
                Console.WriteLine($"Вероятность пневмонии: {result[i]}");
            }

            #endregion
        }
    }
}
