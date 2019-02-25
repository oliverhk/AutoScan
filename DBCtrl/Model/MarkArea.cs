using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class MarkArea
    {
        public int Id { get; set; }
        public string SlideId { get; set; }
        public string SwathId { get; set; }
        public string FieldId { get; set; }
        public int RectX { get; set; }
        public int RectY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string UserId { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }

        public static MarkArea Copy(MarkArea mark)
        {
            MarkArea newMark = new MarkArea();
            newMark.Id = mark.Id;
            newMark.SlideId = mark.SlideId;
            newMark.SwathId = mark.SwathId;
            newMark.FieldId = mark.FieldId;
            newMark.RectX = mark.RectX;
            newMark.RectY = mark.RectY;
            newMark.Width = mark.Width;
            newMark.Height = mark.Height;
            newMark.UserId = mark.UserId;
            newMark.Remarks = mark.Remarks;
            newMark.CreateTime = mark.CreateTime;

            return newMark;
        }
    }
}
