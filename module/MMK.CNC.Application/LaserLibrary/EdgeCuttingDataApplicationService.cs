﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using MMK.CNC.Application.LaserLibrary.Dto;
using MMK.CNC.Core.LaserLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMK.CNC.Application.LaserLibrary
{
    public interface IEdgeCuttingDataApplicationService: IAsyncCrudAppService<EdgeCuttingDataDto,int, EdgeCuttingDataResultRequestDto,CreateEdgeCuttingDataDto,UpdateEdgeCuttingDataDto>
    {
    }
    public class EdgeCuttingDataApplicationService: AsyncCrudAppService<EdgeCuttingData, EdgeCuttingDataDto,int, EdgeCuttingDataResultRequestDto, CreateEdgeCuttingDataDto,UpdateEdgeCuttingDataDto>,IEdgeCuttingDataApplicationService
    {
        public IRepository<Gas, int> GasRepository { set; get; }
        public IRepository<Material, int> MaterialRepository { get; set; }
        public IRepository<MachiningDataGroup, int> MachiningDataGroupRepository { get; set; }
        public IRepository<MachiningKind, int> MachiningKindRepository { get; set; }
        public IRepository<NozzleKind, int> NozzleKindRepository { get; set; }

        IRepository<EdgeCuttingData, int> repository;
        public EdgeCuttingDataApplicationService(IRepository<EdgeCuttingData, int> repository) : base(repository)
        {
            this.repository = repository;
        }

        //protected override IQueryable<EdgeCuttingData> CreateFilteredQuery(EdgeCuttingDataResultRequestDto input)
        //{
        //    return repository.GetAllIncluding().WhereIf(input.MachiningDataGroupId != -1, n => n.MachiningDataGroupId == input.MachiningDataGroupId);
        //}

        //protected override EdgeCuttingDataDto MapToEntityDto(EdgeCuttingData entity)
        //{
        //    var resDto = ObjectMapper.Map<EdgeCuttingDataDto>(entity);
        //    resDto.GasName = GasRepository.FirstOrDefault(d => d.Code == resDto.GasCode)?.Name_CN;

        //    var mGroup = MachiningDataGroupRepository.FirstOrDefault(d => d.Id == resDto.MachiningDataGroupId);

        //    resDto.MachiningKindName = MachiningKindRepository.FirstOrDefault(d => d.Code == resDto.MachiningKindCode)?.Name_CN;
        //    resDto.MaterialName = MaterialRepository.FirstOrDefault(d => d.Code == mGroup.MaterialCode)?.Name_CN;
        //    resDto.NozzleKindName = NozzleKindRepository.FirstOrDefault(d => d.Code == resDto.NozzleKindCode)?.Name_CN;

        //    resDto.MaterialThickness = (double)mGroup?.MaterialThickness;
        //    return resDto;
        //}
        public override async Task<PagedResultDto<EdgeCuttingDataDto>> GetAll(EdgeCuttingDataResultRequestDto input)
        {
            var list = repository.GetAllIncluding().WhereIf(input.MachiningDataGroupId != -1, n => n.MachiningDataGroupId == input.MachiningDataGroupId);
            int count = list.Count();
            var listRes = list.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            var resultDto = ObjectMapper.Map<List<EdgeCuttingDataDto>>(listRes);
            var gasNodes = GasRepository.GetAllIncluding().ToDictionary(d => d.Code);

            var machining = MachiningDataGroupRepository.GetAllIncluding().ToDictionary(d => d.Id);

            var machiningKind = MachiningKindRepository.GetAllIncluding().ToDictionary(d => d.Code);

            var material = MaterialRepository.GetAllIncluding().ToDictionary(d => d.Code);

            var nozzle = NozzleKindRepository.GetAllIncluding().ToDictionary(d => d.Code);

            foreach (var item in resultDto)
            {
                if (gasNodes.ContainsKey(item.GasCode))
                {
                    item.GasName = gasNodes[item.GasCode].Name_CN;
                }

                if (machining.ContainsKey(item.MachiningDataGroupId))
                {
                    var ma = machining[item.MachiningDataGroupId];
                    item.MaterialThickness = machining[item.MachiningDataGroupId].MaterialThickness;
                    if (material.ContainsKey(ma.MaterialCode))
                    {
                        item.MaterialName = material[ma.MaterialCode].Name_CN;
                    }
                }
                if (machiningKind.ContainsKey(item.MachiningKindCode))
                {
                    item.MachiningKindName = machiningKind[item.NozzleKindCode].Name_CN;
                }

                if (nozzle.ContainsKey(item.NozzleKindCode))
                {
                    item.NozzleKindName = nozzle[item.NozzleKindCode].Name_CN;
                }
            }

            await Task.CompletedTask;
            return new PagedResultDto<EdgeCuttingDataDto>(count, resultDto);
        }
    }
}
