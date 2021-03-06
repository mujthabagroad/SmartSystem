﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMK.SmartSystem.WebCommon.HubModel
{
    public class ProgramResolveResultDto
    {
        public string ImagePath { get; set; }
        public string ConnectId { get; set; }
        public string BmpName { get; set; }

        public string Id { get; set; }
        public ProgramDetailDto Data { get; set; }
    }

    public class ProgramDetailDto
    {
        public string FileHash { get; set; }
        public string Name { get; set; }

        public string FullPath { get; set; }

        public double Size { get; set; }

        public string Material { get; set; }

        public double Thickness { get; set; }

        public string Gas { get; set; }

        public double FocalPosition { get; set; }

        public string NozzleKind { get; set; }

        public double NozzleDiameter { get; set; }

        public string PlateSize { get; set; }

        public string UsedPlateSize { get; set; }

        public double PlateSize_W { get; set; }

        public double PlateSize_H { get; set; }

        public double UsedPlateSize_W { get; set; }

        public double UsedPlateSize_H { get; set; }

        public double Max_X { get; set; }
        public double Max_Y { get; set; }
        public double Min_X { get; set; }
        public double Min_Y { get; set; }


        public double CuttingDistance { get; set; }

        public int PiercingCount { get; set; }

        public double CuttingTime { get; set; }

        public int ThumbnaiType { get; set; }

        public string ThumbnaiInfo { get; set; }

        public DateTime UpdateTime { get; set; }

        public override string ToString()
        {
            return $"{Name} {FullPath} {ThumbnaiInfo} {Size}";
        }
    }
}
