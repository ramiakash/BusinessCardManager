import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessCardDashboardComponent } from './business-card-dashboard.component';

describe('BusinessCardDashboardComponent', () => {
  let component: BusinessCardDashboardComponent;
  let fixture: ComponentFixture<BusinessCardDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BusinessCardDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusinessCardDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
