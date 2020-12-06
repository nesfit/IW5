import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { FoodType, RecipeListModel } from '../api/models';
import { RecipeService } from '../api/services';

import { RecipePageComponent } from './recipe-page.component';

describe('RecipePageComponent', () => {
  let component: RecipePageComponent;
  let fixture: ComponentFixture<RecipePageComponent>;

  const recipe1: RecipeListModel =
    { id: 'fabde0cd-eefe-443f-baf6-3d96cc2cbf2e', duration: '00:15:00', foodType: FoodType.MainDish, foodTypeText: 'Hlavní chod', name: 'Míchaná vejce' };
  const recipesArray: Array<RecipeListModel> = [recipe1];
  const recipesObservable: Observable<Array<RecipeListModel>> = of(recipesArray);

  const recipeService = jasmine.createSpyObj('RecipeService', ['recipeGetAll']);
  let getAllSpy: jasmine.Spy;

  beforeEach(async () => {
    getAllSpy = recipeService.recipeGetAll.and.callFake((_x: any) => recipesObservable);

    await TestBed.configureTestingModule({
      declarations: [RecipePageComponent],
      providers: [
        { provide: RecipeService, useValue: recipeService },
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
