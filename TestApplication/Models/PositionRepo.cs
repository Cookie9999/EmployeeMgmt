using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestApplication.Context;

namespace TestApplication.Models
{

    public class PositionRepo : IPositionRepo
    {
        private hSenidEntities dbContext_;
        public PositionRepo(hSenidEntities dbContext)
        {
            dbContext_ = dbContext;
        }
        public bool CreatePosition(PositionDTO position)
        {
            Position positionCreate = new Position
            {
                PositionDescription = position.PositionDescription
            };
            dbContext_.Positions.Add(positionCreate);
            return Save();
        }
     
        public bool DeletePosition(int position_id)
        {
            var result = dbContext_.Positions.Where(a => a.PositionId == position_id).FirstOrDefault();
            dbContext_.Positions.Remove(result);
            return Save();
        }

        public PositionDTO GetPosition(int position_id)
        {
            PositionDTO positionDTO = new PositionDTO();
            var result = dbContext_.Positions.Where(a => a.PositionId == position_id).FirstOrDefault();
            positionDTO.PositionId = result.PositionId;
            positionDTO.PositionDescription = result.PositionDescription;
            return positionDTO;
        }

        public ICollection<PositionDTO> GetPositions()
        {
            var result = dbContext_.Positions.OrderBy(a => a.PositionId).ToList();
            List<PositionDTO> positionDTOs = new List<PositionDTO>();
            foreach (var item in result)
            {
                PositionDTO positionDTO = new PositionDTO();
                positionDTO.PositionId = item.PositionId;
                positionDTO.PositionDescription = item.PositionDescription;
                positionDTOs.Add(positionDTO);
            }
            return positionDTOs; 
        }

        public bool UpdatePosition(PositionDTO position)
        {
            Position positionUpdate = new Position();
            positionUpdate.PositionId = position.PositionId;
            positionUpdate.PositionDescription = position.PositionDescription;
            dbContext_.Entry(positionUpdate).State = EntityState.Modified;
            return Save();
        }
        public bool Save()
        {
            var save = dbContext_.SaveChanges();
            return save >= 0 ? true : false;
        }
    }
}