using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRepository.LibraryRepository.Interface
{



    /// <summary>
    /// Интерфейс с событиями до которых в процессе разработки мможно привязать или назначить обработчик
    /// </summary>
    public interface IEventHandler
    {
        event EventHandler<Exception> EventWithException;
        event EventHandler<List<string>> EventWithList;
    }
}
