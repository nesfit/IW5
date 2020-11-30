import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngredientPageComponent } from './ingredient-page.component';

describe('IngredientPageComponent', () => {
  let component: IngredientPageComponent;
  let fixture: ComponentFixture<IngredientPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IngredientPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngredientPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
