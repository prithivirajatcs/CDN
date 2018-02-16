using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EUJIT
{


    public interface ISaveToGallery
    {
        void sendPhoto(byte[] photo);
    }
}
