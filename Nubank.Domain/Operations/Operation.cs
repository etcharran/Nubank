using Nubank.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nubank.Domain.Operations
{
    public abstract class Operation<T> : IOperation, IOperation<T> where T:IData
    {
        public T Data { get; set; }

        private bool HasBeenBuilt { get; set; }

        public IOperation<T> Build(T data)
        {
            Data = data;
            HasBeenBuilt = true;
            return this;
        }

        public void Process() 
        {
            if (this.HasBeenBuilt)
                this.Execute();
            else
                throw new Exception("The process hasn't been built yet");
        }

        public abstract void Execute();
    }
}
