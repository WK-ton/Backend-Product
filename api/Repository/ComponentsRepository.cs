using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace api.Repository
{
    public class ComponentsRepository : IComponentsRepository
    {
        private readonly AppDbContext _AppdbContext;

        public ComponentsRepository(AppDbContext AppDbContext)
        {
            _AppdbContext = AppDbContext;
        }

        public async Task<Result> ComponentDelete(Cars data, string? action)
        {
            try
            {
                switch (action)
                {
                    case "BangKhen":
                        var BangKhenDelete = await _AppdbContext.BangKhen.FirstOrDefaultAsync(x => x.id == data.id);
                        if (BangKhenDelete != null)
                        {
                            _AppdbContext.BangKhen.Remove(BangKhenDelete);

                            await _AppdbContext.SaveChangesAsync();

                            await _AppdbContext.Database.ExecuteSqlInterpolatedAsync($"ALTER TABLE BangKhen AUTO_INCREMENT = 1");


                            return new Result
                            {
                                success = true,
                                result = BangKhenDelete
                            };

                        }
                        else
                        {
                            return null!;
                        }

                    case "Monument":


                        var MonumentDelete = await _AppdbContext.Monument.FirstOrDefaultAsync(x => x.id == data.id);
                        if (MonumentDelete != null)
                        {
                            _AppdbContext.Monument.Remove(MonumentDelete);


                            await _AppdbContext.SaveChangesAsync();

                            await _AppdbContext.Database.ExecuteSqlInterpolatedAsync($"ALTER TABLE Monument AUTO_INCREMENT = 1");


                            return new Result
                            {
                                success = true,
                                result = MonumentDelete
                            };

                        }
                        else
                        {
                            return null!;
                        }

                    case "Morchit":

                        var MorchitDelete = await _AppdbContext.Morchit.FirstOrDefaultAsync(x => x.id == data.id);
                        if (MorchitDelete != null)
                        {
                            _AppdbContext.Morchit.Remove(MorchitDelete);


                            await _AppdbContext.SaveChangesAsync();

                            await _AppdbContext.Database.ExecuteSqlInterpolatedAsync($"ALTER TABLE Morchit AUTO_INCREMENT = 1");


                            return new Result
                            {
                                success = true,
                                result = MorchitDelete
                            };

                        }
                        else
                        {
                            return null!;
                        }
                }

                return new Result
                {
                    success = false,
                    result = "เกิดข้อผิดพลาดในการ ดึงข้อมูล"
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error on DeleteData" + ex.Message);
            }
        }

        public async Task<Result> ComponentGetData(string? action)
        {
            try
            {
                switch (action)
                {
                    case "BangKhen":

                        var resBangKhen = _AppdbContext.BangKhen.Select(x => new
                        {
                            x.id,
                            x.firstStation,
                            x.lastStation,
                            x.number,
                            x.timeOut,
                            x.roadImage
                        }).ToList().Distinct();

                        if (resBangKhen != null)
                        {
                            return new Result
                            {
                                success = true,
                                result = resBangKhen

                            };
                        }
                        else
                        {
                            return null!;
                        }

                    case "Monument":

                        var resMonument = _AppdbContext.Monument.Select(x => new
                        {
                            x.id,
                            x.firstStation,
                            x.lastStation,
                            x.number,
                            x.timeOut,
                            x.roadImage
                        }).ToList().Distinct();

                        if (resMonument != null)
                        {
                            return new Result
                            {
                                success = true,
                                result = resMonument

                            };
                        }
                        else
                        {
                            return null!;
                        }

                    case "Morchit":

                        var resMorchit = _AppdbContext.Morchit.Select(x => new
                        {
                            x.id,
                            x.firstStation,
                            x.lastStation,
                            x.number,
                            x.timeOut,
                            x.roadImage
                        }).ToList().Distinct();

                        if (resMorchit != null)
                        {
                            return new Result
                            {
                                success = true,
                                result = resMorchit

                            };
                        }
                        else
                        {
                            return null!;
                        }

                }

                return new Result
                {
                    success = false,
                    result = "เกิดข้อผิดพลาดในการ ดึงข้อมูล"
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error on GetMainCar" + ex.Message);
            }
        }

        public async Task<Result> ComponentGetByID(int? id, string? action)
        {
            try
            {
                switch (action)
                {
                    case "BangKhen":
                        var resBangKhen = _AppdbContext.BangKhen.Where(x => x.id == id).ToList();

                        if (resBangKhen != null && resBangKhen.Count > 0)
                        {
                            return new Result
                            {
                                success = true,
                                result = resBangKhen
                            };
                        }
                        else
                        {
                            return null!;
                        }

                    case "Monument":
                        var resMonument = _AppdbContext.Monument.Where(x => x.id == id).ToList();

                        if (resMonument != null && resMonument.Count > 0)
                        {
                            return new Result
                            {
                                success = true,
                                result = resMonument
                            };
                        }
                        else
                        {
                            return null!;
                        }

                    case "Morchit":
                        var resMorchit = _AppdbContext.Morchit.Where(x => x.id == id).ToList();

                        if (resMorchit != null && resMorchit.Count > 0)
                        {
                            return new Result
                            {
                                success = true,
                                result = resMorchit
                            };
                        }
                        else
                        {
                            return null!;
                        }

                }


                return new Result
                {
                    success = false,
                    result = "เกิดข้อผิดพลาดในการ ดึงข้อมูล"
                };


            }
            catch (Exception ex)
            {
                throw new Exception("Error on GetByID" + ex.Message);
            }
        }
    }
}