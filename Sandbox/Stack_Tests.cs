using System;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class Stack_Tests
    {
        [Test]
        public void can_create_a_generic_stack()
        {
            Stack<int> stack = new Stack<int>(5);
            stack.Should().BeOfType<Stack<int>>();
        }

        [Test]
        public void can_create_a_stack()
        {
            Stack stack = new Stack(2);
            stack.Should().BeOfType<Stack>();
        }

        [Test]
        public void can_get_stack_size()
        {
            Stack stack = new Stack(2);
            stack.Size.Should().Be(2);
        }

        [Test]
        public void can_push_onto_the_stack_as_many_times_as_size()
        {
            Stack stack = new Stack(3);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.Pass();
        }

        [Test]
        public void cannot_create_an_empty_stack()
        {
            Action action = () => { new Stack(0); };
            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Test]
        public void popping_an_item_off_the_stack_returns_items_in_reverse_order()
        {
            Stack stack = new Stack(3);
            stack.Push(1);
            stack.Push(2);

            object two = stack.Pop();
            object one = stack.Pop();

            one.Should().Be(1);
            two.Should().Be(2);
        }

        [Test]
        public void pushing_too_many_items_onto_the_stack_should_throw_an_exception()
        {
            Stack stack = new Stack(3);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Action action = () => { stack.Push(4); };

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
