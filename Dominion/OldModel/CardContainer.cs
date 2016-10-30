using System.Collections.Generic;
using System.Linq;

namespace Dominion.OldModel
{
    public class CardContainer : IList<Card>, IEnumerable<Card>
    {
        private readonly List<Card> _cards = new List<Card>();


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
        }

        public void AddToBottom(Card item)
        {
            _cards.Add(item);
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
            AddToTop(item);
        }

        public Card Draw()
        {
            if (_cards.Count == 0)
                return null;

            Card retval = _cards[_cards.Count - 1];
            _cards.RemoveAt(_cards.Count - 1);
            return retval;
        }

        public IList<Card> Draw(int count)
        {
            if (_cards.Count == 0)
                return new List<Card>();

            List<Card> retval = new List<Card>(_cards.Take(count));
            _cards.RemoveRange(0, retval.Count);
            return retval;
        }

        public void AddRange(IEnumerable<Card> cards)
        {
            foreach (var c in cards)
                Add(c);
        }
    }
}