﻿using Base.Persistence.Paging;

namespace Base.Application.Responses
{
    public class GetListResponse<T> : BasePageableModel
    {
        private IList<T> _items;

        public IList<T> Items
        {
            get => _items ??= new List<T>();
            set => _items = value;
        }

    }
}