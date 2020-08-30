using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Context;

namespace TestApplication.Models
{
    public interface IPositionRepo
    {
        ICollection<PositionDTO> GetPositions();
        PositionDTO GetPosition(int position_id);
        bool CreatePosition(PositionDTO position);
        bool UpdatePosition(PositionDTO position);
        bool DeletePosition(int position_id);
        bool Save();
    }
}
