using System;

namespace Sandbox
{
    public class Stack
    {
        private readonly int maxStackPointer;
        private readonly int size;
        private int stackPointer = -1;
        private readonly object[] storage;

        public Stack(int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException("size", "Size cannot be less than 1.");

            this.size = size;
            maxStackPointer = size - 1;
            storage = new object[size];
        }

        public int Size
        {
            get { return size; }
        }

        public void Push(object item)
        {
            if (stackPointer < maxStackPointer)
            {
                stackPointer += 1;
                storage[stackPointer] = item;
                return;
            }
            throw new InvalidOperationException("The stack is full!");
        }

        public object Pop()
        {
            if (stackPointer >= 0)
            {
                object item = storage[stackPointer];
                stackPointer -= 1;
                return item;
            }
            throw new InvalidOperationException("The stack is empty!");
        }
    }

    public class Stack<T>
    {
        private readonly int maxStackPointer;
        private readonly int size;
        private int stackPointer = -1;
        private readonly T[] storage;

        public Stack(int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException("size", "Size cannot be less than 1.");

            this.size = size;
            maxStackPointer = size - 1;
            storage = new T[size];
        }

        public int Size
        {
            get { return size; }
        }

        public void Push(T item)
        {
            if (stackPointer < maxStackPointer)
            {
                stackPointer += 1;
                storage[stackPointer] = item;
                return;
            }
            throw new InvalidOperationException("The stack is full!");
        }

        public T Pop()
        {
            if (stackPointer >= 0)
            {
                T item = storage[stackPointer];
                stackPointer -= 1;
                return item;
            }
            throw new InvalidOperationException("The stack is empty!");
        }
    }
}
