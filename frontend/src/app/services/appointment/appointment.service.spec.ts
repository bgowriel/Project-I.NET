import { HttpClient, HttpClientModule, HttpHandler } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";
import { Appointment } from "app/shared/models/appointment.model";
import { Observable } from "rxjs";

import { AppointmentService } from "./appointment.service";

describe("AppointmentService", () => {
    beforeEach(() => TestBed.configureTestingModule({
        providers: [HttpClientModule, HttpClient, HttpHandler]
    }));

    it("appointment should be created", () => {
        const service: AppointmentService = TestBed.get(AppointmentService);
        expect(service.createAppointment).toBeTruthy();
    });

    it("appointment should be updated", () => {
        const service: AppointmentService = TestBed.get(AppointmentService);
        expect(service.updateAppointment('1', new Appointment)).toBeTruthy();
    });

    it("appointment should be deleted", () => {
        const service: AppointmentService = TestBed.get(AppointmentService);
        expect(service.deleteAppointment('1')).toBeTruthy();
    });

    it("appointment should be retrieved", () => {
        const service: AppointmentService = TestBed.get(AppointmentService);
        expect(service.getAppointments).toBeTruthy();
    });

    it("appointment should be retrieved by patient id", () => {
        const service: AppointmentService = TestBed.get(AppointmentService);
        expect(service.getAppointmentsByPatientId('1')).toBeTruthy();
    });

    it("appointment should be retrieved by doctor id", () => {
        const service: AppointmentService = TestBed.get(AppointmentService);
        expect(service.getAppointmentsByDoctorId('1')).toBeTruthy();
    });

});


