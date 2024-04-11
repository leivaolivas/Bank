using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public interface IFileRepository
    {
        void OpenOrCreateFile();

        void OpenFile();

        void WriteRecordToFile(Record record);

        string ReadNextRecord();

        void ResetFilePointer();

        void CloseFile();

    }


    public class FileRepository
    {
        private StreamWriter _fileWriter;
        private StreamReader _fileReader;
        private FileStream _fileStream;
        private string _fileName;

        public FileRepository(string fileName)
        {
            _fileName = fileName;
        }

        public void OpenOrCreateFile()
        {
            try
            {
                _fileStream = new FileStream(_fileName, FileMode.OpenOrCreate,
                    FileAccess.Write);

                _fileWriter = new StreamWriter(_fileStream);
            }
            catch (IOException)
            {
                throw new IOException("Error al abrir el archivo");
            }
        }

        public void WriteRecordToFile(Record record)
        {
            try
            {
                _fileWriter?.WriteLine($"{record.Account},{record.FirstName}," +
                    $"{record.LastName}, {record.Balance}");
            }
            catch (IOException)
            {
                throw new IOException("Error al escribir en archivo");
            }
        }

        public string ReadNextRecord()
        {
            try
            {
                return _fileReader?.ReadLine();
            }
            catch (IOException)
            {
                throw new IOException("Error al leer del archivo");
            }
        }


        public void ResetFilePointer()
        {
            try
            {
                _fileStream?.Seek(0, SeekOrigin.Begin);
            }
            catch (IOException)
            {
                throw new IOException("Error al restablecer el puntero del archivo");
            }


        }

        public void CloseFile()
        {

            try
            {
                _fileWriter?.Close();
                _fileReader?.Close();
            }
            catch (IOException)
            {
                throw new IOException("No se puede cerrar el archivo");
            }
        }

    }
}
