using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Referência adicionada para trabalhar com a classe Bitmap
using System.Drawing;


namespace FiltrosSequenciais
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Filtros Sequenciais";

            //Define endereço da imagem a ser trabalhada
            Bitmap imgEntrada = (Bitmap)Image.FromFile(@"C:\Users\Tamillys\Desktop\lena.jpg");

            Console.WriteLine("\nExecutando Filtro da Média");
            //Chama função que executa filtro da Média
            meanFilter(imgEntrada);
            Console.WriteLine("\nOk\n");

            Console.WriteLine("\nExecutando Filtro da Mediana");
            //Chama função que executa filtro da Mediana
            medianFilter(imgEntrada);
            Console.WriteLine("\nOk\n");

            Console.WriteLine("\nExecutando Filtro Gaussiano");
            //Chama função que executa filtro Gaussiano
            gaussianFilter(imgEntrada);
            Console.WriteLine("\nOk\n");

            Console.WriteLine("\n\n\nPressione qualquer tecla para sair.\n");
            Console.ReadLine();
        }

        static void meanFilter(Bitmap imgSaida)
        {
            //Cria imagem temporária (igual a original) onde será aplicado o filtro, para facilitar consulta aos valores originais
            Bitmap img = imgSaida;

            //Não altera os pixels da borda para facilitar os cálculos
            for(int j = 1; j < img.Height-2; j++)
            {
                for(int i = 1; i < img.Width-2; i++)
                {
                    //Soma os valores do componete R de cada elemento da matriz 3x3, e ao final calcula média dos valores de R
                    int red = (img.GetPixel(i, j).R) + (img.GetPixel(i, j - 1).R) + (img.GetPixel(i, j + 1).R) + (img.GetPixel(i - 1, j).R) +
                              (img.GetPixel(i + 1, j).R) + (img.GetPixel(i - 1, j + 1).R) + (img.GetPixel(i + 1, j + 1).R) + (img.GetPixel(i + 1, j - 1).R) + (img.GetPixel(i - 1, j - 1).R);
                    
                    red = (int)(red/9);

                    //Faz o mesmo para os componentes G e B
                    int green = (img.GetPixel(i, j).G) + (img.GetPixel(i, j - 1).G) + (img.GetPixel(i, j + 1).G) + (img.GetPixel(i - 1, j).G) + (img.GetPixel(i + 1, j).G) +
                                (img.GetPixel(i - 1, j + 1).G) + (img.GetPixel(i + 1, j + 1).G) + (img.GetPixel(i + 1, j - 1).G) + (img.GetPixel(i - 1, j - 1).G);

                    green = (int)(green/9);

                    int blue = (img.GetPixel(i, j).B) + (img.GetPixel(i, j - 1).B) + (img.GetPixel(i, j + 1).B) + (img.GetPixel(i - 1, j).B) + (img.GetPixel(i + 1, j).B) +
                               (img.GetPixel(i - 1, j + 1).B) + (img.GetPixel(i + 1, j + 1).B) + (img.GetPixel(i + 1, j - 1).B) + (img.GetPixel(i - 1, j - 1).B);

                    blue = (int)(blue/9);

                    //Define o novo valor RGB na imagem sendo filtrada
                    Color rgbColor = Color.FromArgb ( red, green, blue );
                    img.SetPixel( i, j, rgbColor );
                }
            }
            imgSaida = img;

            //Salva imagem filtrada
            imgSaida.Save(@"C:\Users\Tamillys\Desktop\lenaMedia.jpg");
        }

        static void medianFilter(Bitmap imgSaida)
        {
            //Cria imagem temporária (igual a original) onde será aplicado o filtro, para facilitar consulta aos valores originais
            Bitmap img = imgSaida;

            //Não altera os pixels da borda para facilitar os cálculos
            for(int j = 1; j < img.Height-2; j++)
            {
                for(int i = 1; i < img.Width-2; i++)
                {
                    //Armazena em um vetor os valores do componete R de cada elemento da matriz 3x3
                    int[] reds = {(img.GetPixel( i, j ).R), (img.GetPixel( i, j-1 ).R), (img.GetPixel( i, j+1 ).R), (img.GetPixel( i-1, j ).R),
                        (img.GetPixel( i+1, j ).R), (img.GetPixel( i-1, j+1 ).R), (img.GetPixel( i+1, j+1 ).R), (img.GetPixel( i+1, j-1 ).R), (img.GetPixel( i-1, j-1 ).R)};

                    //Ordena os valores em ordem crescente e seleciona o valor que se encontra no meio do vetor
                    int[] redValues = reds.ToArray();
                    Array.Sort(redValues);
                    int redMedianValue = redValues[4];

                    //Repete as mesmas operações para os componentes G e B
                    int[] greens = {(img.GetPixel( i, j ).G), (img.GetPixel( i, j-1 ).G), (img.GetPixel( i, j+1 ).G), (img.GetPixel( i-1, j ).G),
                        (img.GetPixel( i+1, j ).G), (img.GetPixel( i-1, j+1 ).G), (img.GetPixel( i+1, j+1 ).G), (img.GetPixel( i+1, j-1 ).G), (img.GetPixel( i-1, j-1 ).G)};

                    int[] greenValues = greens.ToArray();
                    Array.Sort(greenValues);
                    int greenMedianValue = greenValues[4];

                    int[] blues = {(img.GetPixel( i, j ).B), (img.GetPixel( i, j-1 ).B), (img.GetPixel( i, j+1 ).B), (img.GetPixel( i-1, j ).B),
                        (img.GetPixel( i+1, j ).B), (img.GetPixel( i-1, j+1 ).B), (img.GetPixel( i+1, j+1 ).B), (img.GetPixel( i+1, j-1 ).B), (img.GetPixel( i-1, j-1 ).B)};

                    int[] blueValues = blues.ToArray();
                    Array.Sort(blueValues);
                    int blueMedianValue = blueValues[4];


                    //Define o novo valor RGB na imagem sendo filtrada.
                    Color rgbColor = Color.FromArgb( redMedianValue, greenMedianValue, blueMedianValue );
                    img.SetPixel( i, j, rgbColor );
                }
            }
            imgSaida = img;

            //Salva imagem filtrada
            imgSaida.Save(@"C:\Users\Tamillys\Desktop\lenaMediana.jpg");
        }

        static void gaussianFilter(Bitmap imgSaida)
        {
            //Cria imagem temporária (igual a original) onde será aplicado o filtro, para facilitar consulta aos valores originais
            Bitmap img = imgSaida;

            //Não altera os pixels da borda para facilitar os cálculos
            for (int j = 1; j < img.Height - 2; j++)
            {
                for (int i = 1; i < img.Width - 2; i++)
                {
                    //Realiza operações utilizando a matriz Gaussiana nos componentes R de cada elemento da matriz
                    //A matriz Gaussiana é composta pelos valores 4, 2 e 1 multiplicados abaixo 
                    int red = 4*(img.GetPixel( i, j ).R) +
                              2 * ((img.GetPixel(i, j - 1).R) + (img.GetPixel(i, j + 1).R) + (img.GetPixel(i - 1, j).R) + (img.GetPixel(i + 1, j).R)) +
                              1 * ((img.GetPixel(i - 1, j + 1).R) + (img.GetPixel(i + 1, j + 1).R) + (img.GetPixel(i + 1, j - 1).R) + (img.GetPixel(i - 1, j - 1).R));
                    
                    //Calcula média ponderada dos valores de R
                    red = (int)(red/16);

                    //Repete as mesmas operações para os componentes G e B
                    int green = 4*(img.GetPixel( i, j ).G) +
                              2 * ((img.GetPixel(i, j - 1).G) + (img.GetPixel(i, j + 1).G) + (img.GetPixel(i - 1, j).G) + (img.GetPixel(i + 1, j).G)) +
                              1 * ((img.GetPixel(i - 1, j + 1).G) + (img.GetPixel(i + 1, j + 1).G) + (img.GetPixel(i + 1, j - 1).G) + (img.GetPixel(i - 1, j - 1).G));

                    green = (int)(green/16);

                    int blue = 4*(img.GetPixel( i, j ).B) +
                              2 * ((img.GetPixel(i, j - 1).B) + (img.GetPixel(i, j + 1).B) + (img.GetPixel(i - 1, j).B) + (img.GetPixel(i + 1, j).B)) +
                              1 * ((img.GetPixel(i - 1, j + 1).B) + (img.GetPixel(i + 1, j + 1).B) + (img.GetPixel(i + 1, j - 1).B) + (img.GetPixel(i - 1, j - 1).B));

                    blue = (int)(blue/16);

                    //Define o novo valor RGB na imagem sendo filtrada.
                    Color rgbColor = Color.FromArgb(red, green, blue);
                    img.SetPixel(i, j, rgbColor);
                }
            }
            imgSaida = img;

            //Salva imagem filtrada
            imgSaida.Save(@"C:\Users\Tamillys\Desktop\lenaGaussian.jpg");
        }

    }
}
