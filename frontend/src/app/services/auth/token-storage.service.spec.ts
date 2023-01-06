// add tests for token-storage.service.ts
import { TestBed } from "@angular/core/testing";
import { TokenStorageService } from "./token-storage.service";

describe("TokenStorageService", () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it("should be created", () => {
        const service: TokenStorageService = TestBed.get(TokenStorageService);
        expect(service).toBeTruthy();
    });

    it("should be able to save token", () => {
        const service: TokenStorageService = TestBed.get(TokenStorageService);
        expect(service.saveToken('token')).toBeTruthy();
    });

    it("should be able to save user", () => {
        const service: TokenStorageService = TestBed.get(TokenStorageService);
        expect(service.saveUser({})).toBeTruthy();
    });

    it("should be able to get token", () => {
        const service: TokenStorageService = TestBed.get(TokenStorageService);
        expect(service.getToken()).toBeTruthy();
    });
});