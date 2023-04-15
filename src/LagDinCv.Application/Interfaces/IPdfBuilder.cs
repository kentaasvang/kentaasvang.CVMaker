using LagDinCv.Domain.Requests;

namespace LagDinCv.Application.Interfaces;

public interface IPdfBuilder
{
    Task<Uri> CreateResume(CreateCvRequest model);
}
