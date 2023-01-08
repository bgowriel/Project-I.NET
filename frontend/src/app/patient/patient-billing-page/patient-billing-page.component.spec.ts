import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientBillingPageComponent } from './patient-billing-page.component';

describe('PatientBillingPageComponent', () => {
  let component: PatientBillingPageComponent;
  let fixture: ComponentFixture<PatientBillingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientBillingPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientBillingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
