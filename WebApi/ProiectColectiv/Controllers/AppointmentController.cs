﻿using Microsoft.AspNetCore.Mvc;
using ProiectColectiv.Data;
using ProiectColectiv.Services;

namespace ProiectColectiv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentService _appointmentService;

        public ILogger<AppointmentController> Logger => _logger;

        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public ActionResult Post(AppointmentPostDTO appointment)
        {
            _appointmentService.AddAppointment(appointment);

            return new JsonResult(appointment);
        }

        [HttpGet]
        public IEnumerable<Appointment> GetAll(string userEmail)
        {
            var appointments = _appointmentService.GetAllAppointments(userEmail);
            return appointments;
        }

        [HttpGet("{appointmentId}")]
        public Appointment GetAppointmentById(int appointmentId)
        {
            return _appointmentService.GetAppointmentById(appointmentId);
        }

        [HttpPut("{appointmentId}")]
        public ActionResult Update([FromBody] AppointmentPostDTO appointment, int appointmentId)
        {
            _appointmentService.UpdateAppointment(appointment, appointmentId);

            return new JsonResult(true);
        }

        [HttpDelete]
        public ActionResult<Appointment> Delete([FromBody] int id)
        {
            _appointmentService.DeleteAppointment(id);

            return NoContent();
        }
    }
}