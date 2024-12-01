﻿using APITransfer.DTOs;
using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeRepository _homeRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly IPickupRepository _pickupRepository;
        private readonly IStoreRepository _storeRepository;


        public HomeController(
            IHomeRepository homeRepository, 
            IAgencyRepository agencyRepository, 
            IHotelRepository hotelRepository,
            IUnitRepository unitRepository,
            IZoneRepository zoneRepository,
            IPickupRepository pickupRepository,
            IStoreRepository storeRepository
            )

        {
            _homeRepository = homeRepository;
            _agencyRepository = agencyRepository;
            _hotelRepository = hotelRepository;
            _unitRepository = unitRepository;
            _zoneRepository = zoneRepository;
            _pickupRepository = pickupRepository;
            _storeRepository = storeRepository;
        }

        [HttpGet("get-all-info")]
        public async Task<IActionResult> GetAllInfo()
        {
            try
            {
                var agencies = await _agencyRepository.GetAllAgenciesAsync();
                var hotels = await _hotelRepository.GetAllHotelsAsync();
                var units = await _unitRepository.GetAllUnitsAsync();
                var zones = await _zoneRepository.GetAllZonesAsync();
                var pickups = await _pickupRepository.GetAllPickupsAsync();
                var stores = await _storeRepository.GetAllStoresAsync();

                var response = new
                {
                    Agencies = agencies,
                    Hotels = hotels,
                    Stores = stores,
                    Units = units,
                    Zones = zones,
                    Pickups = pickups
                };

                return Ok(new ResponseHelper<object>(true, "Información recuperada con éxito", response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHelper<string>(false, "Error al recuperar la información", ex.Message));
            }
        }
    }
}
