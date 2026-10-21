using System;

namespace SQLRepository.libraryException
{
    /// <summary>
    /// Ошибка по случаю передачи пустого параметра
    /// </summary>
    public class NullParametersException : Exception
    {
        /// <summary>
        /// Поле названия ошибки
        /// </summary>
        private string name = "NullParametersException"; 
        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        private string message = "Ошибка вызвана из за передачи ссылки на обект который является Null ";
        /// <summary>
        /// Описание ошибки
        /// </summary>
        private string description;
        /// <summary>
        /// Тип серьезности ошибки
        /// </summary>
        private string type_exception_level;
        public string Name { get { return this.name; } }
        public string Message { get { return this.message; } }
        public string Description { get { return this.description; } }
        public string Type_exception_level { get { return this.type_exception_level; } }

        public NullParametersException(string name, string message, string description, string type_exception_level) : base()
        {
            this.name = name;
            this.message = message;
            this.description = description;
            this.type_exception_level = type_exception_level;
        }
    }
}
