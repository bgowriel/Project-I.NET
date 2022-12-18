import { HttpClient, HttpClientModule, HttpHandler } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";

import { PatientService } from "./patient.service";

describe("PatientService", () => {
    beforeEach(() => TestBed.configureTestingModule({
        providers: [HttpClientModule, HttpClient, HttpHandler]
    }));

    it("patient should be updated", () => {
        const service: PatientService = TestBed.get(PatientService);
        expect(service.updatePatient).toBeTruthy();
    });

    it("patient should be retrieved by id", () => {
        const service: PatientService = TestBed.get(PatientService);
        expect(service.getPatientById).toBeTruthy();
    });

});


