import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FactorymanagerComponent } from './factorymanager.component';

describe('FactorymanagerComponent', () => {
  let component: FactorymanagerComponent;
  let fixture: ComponentFixture<FactorymanagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FactorymanagerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FactorymanagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
