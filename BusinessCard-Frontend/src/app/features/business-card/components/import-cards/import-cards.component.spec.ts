import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportCardsComponent } from './import-cards.component';

describe('ImportCardsComponent', () => {
  let component: ImportCardsComponent;
  let fixture: ComponentFixture<ImportCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ImportCardsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ImportCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
