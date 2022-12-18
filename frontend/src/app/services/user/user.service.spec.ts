import { HttpClient, HttpClientModule, HttpHandler } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";
import { TokenStorageService } from "../auth/token-storage.service";

import { UserService } from "./user.service";

describe("UserService", () => {
    beforeEach(() => TestBed.configureTestingModule({
        providers: [TokenStorageService]
    }));

    it("user should be decoded", () => {
        const service: UserService = TestBed.get(UserService);
        expect(service.getUser).toBeTruthy();
    });

});


