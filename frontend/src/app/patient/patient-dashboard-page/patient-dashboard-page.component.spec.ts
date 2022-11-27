import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientDashboardPageComponent } from './patient-dashboard-page.component';

describe('PatientDashboardPageComponent', () => {
  let component: PatientDashboardPageComponent;
  let fixture: ComponentFixture<PatientDashboardPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientDashboardPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientDashboardPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
