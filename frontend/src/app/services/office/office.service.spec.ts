import { HttpClient, HttpClientModule, HttpHandler } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";

import { OfficeService } from "./office.service";

describe("OfficeService", () => {
    beforeEach(() => TestBed.configureTestingModule({
        providers: [HttpClientModule, HttpClient, HttpHandler]
    }));

    it("office should be retrieved by id", () => {
        const service: OfficeService = TestBed.get(OfficeService);
        expect(service.getOfficeById('1')).toBeTruthy();
    });

    it("should retrieve all offices", () => {
        const service: OfficeService = TestBed.get(OfficeService);
        expect(service.getAllOffices).toBeTruthy();
    });

    it("should update office", () => {
        const service: OfficeService = TestBed.get(OfficeService);
        expect(service.updateOffice).toBeTruthy();
    }); 

    it("should delete office", () => {
        const service: OfficeService = TestBed.get(OfficeService);
        expect(service.deleteOffice).toBeTruthy();
    });

    it("should create office", () => {
        const service: OfficeService = TestBed.get(OfficeService);
        expect(service.registerOffice).toBeTruthy();
    });

    it("should assign doctor to office", () => {
        const service: OfficeService = TestBed.get(OfficeService);
        expect(service.assignDoctorToOffice).toBeTruthy();
    });

});


