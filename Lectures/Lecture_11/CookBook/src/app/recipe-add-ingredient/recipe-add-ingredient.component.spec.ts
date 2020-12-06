import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeAddIngredientComponent } from './recipe-add-ingredient.component';

describe('RecipeAddIngredientComponent', () => {
  let component: RecipeAddIngredientComponent;
  let fixture: ComponentFixture<RecipeAddIngredientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecipeAddIngredientComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeAddIngredientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
