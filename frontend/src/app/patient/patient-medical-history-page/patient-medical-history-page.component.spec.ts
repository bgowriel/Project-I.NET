import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientMedicalHistoryPageComponent } from './patient-medical-history-page.component';

describe('PatientMedicalHistoryPageComponent', () => {
  let component: PatientMedicalHistoryPageComponent;
  let fixture: ComponentFixture<PatientMedicalHistoryPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientMedicalHistoryPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientMedicalHistoryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
