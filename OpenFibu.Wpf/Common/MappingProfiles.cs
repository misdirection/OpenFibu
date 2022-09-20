using AutoMapper;
using OpenFibu.Application.DTO;
using OpenFibu.Wpf.Vorkontierung;
using System;

namespace OpenFibu.Wpf.Common;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<VorkontierungViewModel, VorkontierungsDto>()
               .ForMember(dst => dst.Id, z => z.MapFrom(src => src.Id))
               .ForMember(dst => dst.LaufendeNummer, z => z.MapFrom(src => src.LaufendeNummer))
               .ForMember(dst => dst.Belegnummer, z => z.MapFrom(src => src.Belegnummer))
               .ForMember(dst => dst.Belegdatum, z => z.ConvertUsing(new DateOnlyTypeConverter()))
               .ForMember(dst => dst.Buchungsdatum, z => z.ConvertUsing(new DateOnlyTypeConverter()));

        CreateMap<VorkontierungsDto, VorkontierungViewModel>()
                .ForMember(dst => dst.Id, z => z.MapFrom(src => src.Id))
                .ForMember(dst => dst.LaufendeNummer, z => z.MapFrom(src => src.LaufendeNummer))
                .ForMember(dst => dst.Belegnummer, z => z.MapFrom(src => src.Belegnummer))
                .ForMember(dst => dst.Belegdatum, z => z.ConvertUsing(new DateTimeTypeConverter()))
                .ForMember(dst => dst.Buchungsdatum,z => z.ConvertUsing(new DateTimeTypeConverter()));
    }
}

public class DateTimeTypeConverter : IValueConverter<DateOnly, DateTime?>
{
    public DateTime? Convert(DateOnly sourceMember, ResolutionContext context)
    {
        return sourceMember.ToDateTime(TimeOnly.MinValue);
    }
}
public class DateOnlyTypeConverter : IValueConverter<DateTime, DateOnly>
{
    public DateOnly Convert(DateTime sourceMember, ResolutionContext context)
    {
        return DateOnly.FromDateTime(sourceMember);
    }
}