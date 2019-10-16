using System;
using Administration.API.Models.EnumResources;
using Administration.API.Queries.Base;

namespace Administration.API.Models.InputResources
{
    [Serializable]
    public class RoomSearchFilterInput
    {
        public int? Capacity { get; set; }
        public RoomTypeResource? Type { get; set; }
        public RoomStateResource? State { get; set; }
        public Sorting Sorting { get; set; } = Sorting.Asc;
        public RoomFilterSortFields Sort { get; set; }
        public bool WithPaging { get; set; } = true;
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 25;
    }

}