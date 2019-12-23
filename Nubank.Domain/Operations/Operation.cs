using Nubank.Contract;
using Nubank.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nubank.Domain.Operations
{
    public abstract class Operation<T> : IOperation<T> where T:IData
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
            if (this.HasBeenBuilt){
                foreach (var validation in ValidationFixture)
                {
                    validation.Validate();
                }
                this.Execute();
            }
            else
                throw new Exception("The process hasn't been built yet");
        }

        public abstract List<IBusinessValidation> ValidationFixture { get; set; }

        public abstract void Execute();
    }
}
