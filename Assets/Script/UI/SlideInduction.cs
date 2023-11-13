using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;




    public class SlideInduction : MonoBehaviour, IDragHandler
    {
        public  Slider slider;
        private RectTransform handleRect;
        private float handleWidth;


        private void Start()
        {
            slider = GetComponent<Slider>();
            handleRect = slider.handleRect;
            handleWidth = handleRect.rect.width;
        }


        public void OnDrag(PointerEventData eventData)
        {

            if (IsPointerOverHandle(eventData))
            {
                UpdateSliderValue(eventData);

            }

        }

        private bool IsPointerOverHandle(PointerEventData eventData)
        {
            Vector2 handleCenter = handleRect.position + new Vector3(handleWidth / 2, 0);
            return Vector2.Distance(eventData.position, handleCenter) < handleWidth / 2;
        }

        private void UpdateSliderValue(PointerEventData eventData)
        {
            float sliderWidth = slider.GetComponent<RectTransform>().rect.width;
            float normalizedValue = (eventData.position.x - slider.GetComponent<RectTransform>().position.x) / sliderWidth;
            slider.value = Mathf.Clamp01(normalizedValue);
        }
       
    }
