using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.DL.PublicModels
{
    public class ResponseModelData<T>
    {
        public T? Data { get; set; } = typeof(T).GetInterface(nameof(IEnumerable)) != null && typeof(T).IsGenericType ? (T)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(T).GenericTypeArguments[0])) : default;
        public MetaDataDTO MetaData { get; set; } = new MetaDataDTO();
        public ErrorsVM Err { get; set; } = null!;

       
    }
}
