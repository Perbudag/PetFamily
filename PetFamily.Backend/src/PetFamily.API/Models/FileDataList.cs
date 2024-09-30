using PetFamily.Application.Models;
using System.Collections;

namespace PetFamily.API.Models
{

    //В бужущем я переделаю этот класс в FileDtoList
    public class FileDataList : IReadOnlyList<FileData>, IAsyncDisposable
    {
        private readonly List<FileData> _fileDataList;


        public FileDataList(List<FileData> fileDataList)
        {
            _fileDataList = fileDataList;
        }

        public FileDataList(IFormFileCollection formFileCollection)
        {
            _fileDataList = new();

            foreach (var formFile in formFileCollection)
            {

                var location = new FileLocation("test", formFile.FileName);

                var fileData = new FileData(formFile.OpenReadStream(), location);

                _fileDataList.Add(fileData);
            }
        }


        public FileData this[int index] => _fileDataList[index];

        public int Count => _fileDataList.Count;

        public IEnumerator<FileData> GetEnumerator() =>
            _fileDataList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _fileDataList.GetEnumerator();

        public async ValueTask DisposeAsync()
        {
            foreach (var fileData in _fileDataList)
            {
                await fileData.Stream!.DisposeAsync();
            }
        }


        public static implicit operator FileDataList(List<FileData> list) =>
            new FileDataList(list);

        public static implicit operator List<FileData>(FileDataList list) =>
            list._fileDataList;
    }
}
