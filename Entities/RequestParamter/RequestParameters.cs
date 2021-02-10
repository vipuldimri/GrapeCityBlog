using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.RequestParamter
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;

        [Range(1, 1000)]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > maxPageSize) {
                    _pageSize = maxPageSize;
                } else {
                    if (value == 0 ) {
                        _pageSize = 1;
                    } else{
                        _pageSize = value;
                    }
                }
            }
        }
        public string SearchTerm { get; set; }
    }
}
