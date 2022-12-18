import { HttpClient, HttpClientModule, HttpHandler } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";
import { User } from "app/shared/models/user.model";

import { DoctorService } from "./doctor.service";

describe("DoctorService", () => {
    beforeEach(() => TestBed.configureTestingModule({
        providers: [HttpClientModule, HttpClient, HttpHandler]
    }));

    it("doctor should be updated", () => {
        const service: DoctorService = TestBed.get(DoctorService);
        expect(service.updateDoctor(new User())).toBeTruthy();
    });

    it("doctor should be retrieved by id", () => {
        const service: DoctorService = TestBed.get(DoctorService);
        expect(service.getDoctorById('1')).toBeTruthy();
    });

    it("doctor should be retrieved by office id", () => {
        const service: DoctorService = TestBed.get(DoctorService);
        expect(service.getDoctorsByOfficeId('1')).toBeTruthy();
    });
});


