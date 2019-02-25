using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class MarkCell
    {
        public int Id { get; set; }
        public string SlideId { get; set; }
        public string SwathId { get; set; }
        public string FieldId { get; set; }
        public int ImageX { get; set; }
        public int ImageY { get; set; }
        public int StageX { get; set; }
        public int StageY { get; set; }
        public int TypeId { get; set; }
        public int AreaId { get; set; }
        public string UserId { get; set; }
        public string Remarks { get; set; }
        public string SlideFrom { get; set; }
        public DateTime CreateTime { get; set; }
        //public string CategoryName { get; set; }  //for UI show only
        //public string CellTypeName { get; set; }  //for UI show only
        //public string UserLevel { get; set; }  //for UI show only

        public static MarkCell Copy(MarkCell mark)
        {
            MarkCell newMark = new MarkCell();
            newMark.Id = mark.Id;
            newMark.SlideId = mark.SlideId;
            newMark.SwathId = mark.SwathId;
            newMark.FieldId = mark.FieldId;
            newMark.ImageX = mark.ImageX;
            newMark.ImageY = mark.ImageY;
            newMark.StageX = mark.StageX;
            newMark.StageY = mark.StageY;
            newMark.TypeId = mark.TypeId;
            newMark.AreaId = mark.AreaId;
            newMark.UserId = mark.UserId;
            newMark.Remarks = mark.Remarks;
            newMark.SlideFrom = mark.SlideFrom;
            newMark.CreateTime = mark.CreateTime;

            return newMark;
        }
    }
}
