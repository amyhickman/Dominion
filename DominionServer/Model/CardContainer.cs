using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Model
{
    public class CardContainer : IList<Card>
    {
        public int ContainerId { get; set; }
        public int OwnerId { get; set; }


        private List<Card> _cards = new List<Card>();

        #region IList<Card>
        public int IndexOf(Card item)
        {
            return _cards.IndexOf(item);
        }

        public void Insert(int index, Card item)
        {
            _cards.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _cards.RemoveAt(index);
        }

        public Card this[int index]
        {
            get { return _cards[index]; }
            set { _cards[index] = value; }
        }

        public void AddToTop(Card item)
        {
            _cards.Insert(0, item);
            item.Container = this;
        }

        public void AddToBottom(Card item)
        {
            _cards.Add(item);
            item.Container = this;
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public bool Contains(Card item)
        {
            return _cards.Contains(item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            _cards.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _cards.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<Card>)_cards).IsReadOnly; }
        }

        public bool Remove(Card item)
        {
            return _cards.Remove(item);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _cards.GetEnumerator();
        }
        #endregion

        public void Add(Card item)
        {
            throw new Exception("Don't use this method");
        }
    }
}